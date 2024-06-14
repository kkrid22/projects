import express from 'express';
import { existsSync, mkdirSync, unlinkSync } from 'fs';
import multer from 'multer';
import bodyParser from 'body-parser';
import bcrypt from 'bcrypt';
import jwt from 'jsonwebtoken';
import db from '../db/requests.db.js';
import {
  validateTime,
  validateRent,
  validateAddress,
  validateDescription,
} from '../middleware/validator.middleware.js';
import setToken from '../middleware/setToken.js';

const secret = '1c28d07215544bd1b24faccad6c14a04';

const postRouter = express.Router();
const app = express();

app.use(bodyParser.json());

const uploadDir = 'static/pic';
if (!existsSync(uploadDir)) {
  mkdirSync(uploadDir);
}

const multerUpload = multer({
  dest: uploadDir,
  limits: {
    fieldSize: 4 * 1024 * 1024,
  },
});

postRouter.post(
  '/insertField',
  setToken,
  express.urlencoded({ extended: true }),
  validateTime,
  validateRent,
  validateAddress,
  validateDescription,
  async (req, res) => {
    const {
      courtUpload: courtNames,
      description: desc,
      rentingPrice: rent,
      address: adr,
      openingTime,
      closingTime,
    } = req.body;
    let errorMessage = null;
    try {
      const { userData } = req;
      if (userData.role !== 'admin') {
        throw new Error('you have no permission');
      }

      await db.insertCourt({
        Nev: courtNames,
        Ar: rent,
        Cim: adr,
        Leiras: desc,
        NyitasiIdo: openingTime,
        ZarasiIdo: closingTime,
      });
      const requests = await db.findAllCourts();

      res.render('index', { requests, errorMessage });
    } catch (err) {
      errorMessage = err.message;
      const requests = await db.findAllCourts();
      res.render('index', { requests, errorMessage });
    }
  },
);

postRouter.post('/uploadFile', setToken, multerUpload.single('Image'), async (request, response) => {
  try {
    const { userData } = request;
    if (userData.role !== 'admin') {
      throw new Error('you have no permission');
    }

    const fileHandler = request.file;
    if (!fileHandler) {
      throw new Error('No file was uploaded, cannot save nonexistent file');
    }

    if (/[a-zA-Z]/.test(request.body.courtid)) {
      throw new Error('The id cannot contain any letters');
    }

    let { courtid } = request.body;
    courtid = parseInt(courtid, 10);
    await db.insertPicture(courtid, request.file.filename);
    response.redirect(`/details/${courtid}?message=File uploaded successfully&status=success`);
  } catch (error) {
    response.redirect(`/details/${request.body.courtid}?message=${error.message}&status=error`);

    if (request.file && request.file.path) {
      const fileHandler = request.file;
      if (fileHandler) {
        unlinkSync(fileHandler.path);
      }
    }
  }
});

postRouter.post('/rent', setToken, express.urlencoded({ extended: true }), async (req, res) => {
  let { courtid } = req.body;
  const { rentDate } = req.body;

  const currentDate = new Date();
  const selectedDate = new Date(rentDate);
  try {
    if (/[a-zA-Z]/.test(courtid)) {
      throw new Error('could not be modified');
    }

    if (!(selectedDate instanceof Date)) {
      throw new Error('Date is expected');
    }

    if (selectedDate < currentDate) {
      throw new Error('Date is too old');
    }

    const repeated = await db.courtRentedForThatDay(courtid, rentDate);
    if (repeated !== undefined) {
      throw new Error('Court already rented for that day');
    }

    const { userData } = req;
    let userID = await db.findUserID(userData.username);
    userID = userID[0].FelhasznaloID;
    courtid = parseInt(courtid, 10);

    await db.insertReservation(courtid, userID, selectedDate);
    res.redirect(`/details/${courtid}?message=Your reservation is being processed&status=success`);
    return;
  } catch (err) {
    res.redirect(`/details/${courtid}?message=${err.message}&status=error`);
  }
});

postRouter.post('/acceptUser', async (req, res) => {
  const { username } = req.query;
  try {
    await db.changeUserRole(username);
    res.sendStatus(200);
  } catch (err) {
    console.error('error deleteing user:', err);
    res.status(500).send('failed to delete user');
  }
});

postRouter.post('/change', setToken, async (req, res) => {
  let username;
  const { newUsername, newPassword } = req.body;
  const { userData } = req;

  try {
    if (userData && userData !== undefined) {
      username = userData.username;
    } else {
      throw new Error('you are not logged in');
    }

    const userInDatabase = await db.findUser(username);
    if (!userInDatabase || userInDatabase === undefined) {
      throw new Error('there is no such user');
    }
    if (!newUsername || newUsername === undefined) {
      throw new Error('please give a username');
    }

    if (!newPassword || newPassword === undefined) {
      throw new Error('please give a password');
    }
    const doesItExist = await db.findUser(newUsername);
    if (doesItExist !== undefined || !doesItExist) {
      throw new Error('username already in use');
    }
    const saltedPassword = await bcrypt.hash(newPassword, 10);
    await db.updateUser(username, newUsername, saltedPassword);

    res.clearCookie('token');

    const token = jwt.sign({ username: newUsername, role: 'user' }, secret, {
      expiresIn: '1h',
    });
    res.cookie('token', token, { httpOnly: true, maxAge: 3600000 });
    res.redirect('/');
  } catch (err) {
    console.error('something wrong with the changes:', err);
    res.status(500).send('something wrong with the changes:', err);
  }
});

postRouter.post('/acceptReserv', setToken, async (req, res) => {
  const { date, courtID } = req.query;
  const { role } = req.userData;
  try {
    if (role !== 'admin') {
      throw new Error('you cant accept this');
    }

    await db.acceptReservation(courtID, date);
    res.sendStatus(200);
  } catch (err) {
    console.error('error deleteing reservation:', err);
    res.status(500).send('failed to delete reservation');
  }
});

export default postRouter;
