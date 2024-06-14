import express from 'express';
import { existsSync, mkdirSync, unlinkSync } from 'fs';
import bodyParser from 'body-parser';
import path from 'path';
import db from '../db/requests.db.js';
import setToken from '../middleware/setToken.js';

const deleteRouter = express.Router();
const app = express();

app.use(bodyParser.json());

const uploadDir = 'static/pic';
if (!existsSync(uploadDir)) {
  mkdirSync(uploadDir);
}

deleteRouter.delete('/deleteImage', setToken, async (req, res) => {
  const { filename } = req.query;
  const filepath = path.join(uploadDir, filename);
  try {
    unlinkSync(filepath);
    await db.deleteImage(filename);
    res.sendStatus(200);
  } catch (err) {
    console.error('error deleteing image:', err);
    res.status(500).send('failed to delete image');
  }
});

deleteRouter.delete('/deleteReserv', setToken, async (req, res) => {
  const { date, courtID } = req.query;
  const { role } = req.userData;

  try {
    if (role !== 'user' && role !== 'admin') {
      throw new Error('you dont have authority');
    }
    const id = await db.courtRentedForThatDay(courtID, date);
    const userid = id.FelhasznaloID;

    if (id.FelhasznaloID !== userid && role === 'user') {
      throw new Error('you cant delete this reservation');
    }

    await db.deleteReservation(userid, date);
    res.sendStatus(200);
  } catch (err) {
    console.error('error deleteing reservation:', err);
    res.status(500).send('failed to delete reservation');
  }
});

deleteRouter.delete('/deleteUser', async (req, res) => {
  const { username } = req.query;
  try {
    await db.disableUser(username);
    res.sendStatus(200);
  } catch (err) {
    console.error('error deleteing user:', err);
    res.status(500).send('failed to delete user');
  }
});

deleteRouter.delete('/userBlock', setToken, async (req, res) => {
  const { userID } = req.query;

  try {
    await db.blockUser(userID);
    res.sendStatus(200);
  } catch (err) {
    console.error('error blocking user:', err);
    res.status(500).send('failed to block user');
  }
});

export default deleteRouter;
