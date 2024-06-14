import express from 'express';
import { existsSync, mkdirSync } from 'fs';
import bodyParser from 'body-parser';
import db from '../db/requests.db.js';
import setToken from '../middleware/setToken.js';

const getRouter = express.Router();
const app = express();

app.use(bodyParser.json());

const uploadDir = 'static/pic';
if (!existsSync(uploadDir)) {
  mkdirSync(uploadDir);
}

function formatDateForDatabase(dateString) {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0'); // kitomi 0 val ameddig a hossza 2 nem lesz
  const day = String(date.getDate()).padStart(2, '0');
  return `${year}-${month}-${day}`;
}

getRouter.get(['/', '/index'], setToken, express.urlencoded({ extended: true }), async (req, response) => {
  let { courtType, minPrice, maxPrice } = req.query;
  let errorMessage = null;
  const { userData } = req;
  try {
    if (!courtType) {
      courtType = 'whatever';
    }

    minPrice = minPrice ? parseFloat(minPrice) : 0;
    maxPrice = maxPrice ? parseFloat(maxPrice) : Number.MAX_SAFE_INTEGER;

    if (Number.isNaN(minPrice) || Number.isNaN(maxPrice)) {
      throw new Error('Price must be a number');
    }

    if (minPrice < 0 || maxPrice < 0) {
      throw new Error('Min or max price cannot be less than 0');
    }

    if (minPrice > maxPrice) {
      throw new Error('The minimum rent price cannot be higher than the maximum rent price');
    }

    const requests = await db.filterCourts(courtType, minPrice, maxPrice);
    response.render('index', { requests, errorMessage, userData });
  } catch (err) {
    errorMessage = err.message;
    const requests = await db.findAllCourts();
    response.render('index', { requests, errorMessage, userData });
  }
});

getRouter.get('/details/:id', setToken, async (req, res) => {
  const id = parseInt(req.params.id, 10);
  const { minDate, maxDate } = req.query;
  const { message, status } = req.query;
  const errorMessage = null;
  const { userData } = req;
  try {
    const dbStartDate = minDate ? formatDateForDatabase(minDate) : '0000-01-01';
    const dbEndDate = maxDate ? formatDateForDatabase(maxDate) : '9999-12-31';

    const court = await db.findCourtDetails(id);
    const paths = await db.findCourtPhotos(id);
    const reservation = await db.findReservations(id, dbStartDate, dbEndDate);

    res.render('details', { court, paths, reservation, message, status, errorMessage, userData });
  } catch (err) {
    res.render('error', { message: `Selection unsuccessful: ${err.message}` });
  }
});

getRouter.get('/approve', setToken, async (req, res) => {
  try {
    const userInfo = await db.findGuests();
    const { userData } = req;
    console.log(userInfo);
    res.render('approval', { users: userInfo, userData });
  } catch (err) {
    res.render('error', { message: `Selection unsuccessful: ${err.message}` });
  }
});

getRouter.get('/getMoreDetails', async (req, res) => {
  const { id } = req.query;

  try {
    const details = await db.getMissingDetais(id);

    if (!details) {
      throw new Error('Details not found');
    }

    res.json(details);
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
});

getRouter.get('/change-information', setToken, (req, res) => {
  try {
    const { userData } = req;
    res.render('change-information', { userData });
  } catch (err) {
    res.render('error', { message: `could not load page: ${err.message}` });
  }
});

getRouter.get('/admin-court', setToken, (req, res) => {
  try {
    const { userData } = req;
    res.render('admin-court', { userData });
  } catch (err) {
    res.render('error', { message: `could not load page: ${err.message}` });
  }
});

getRouter.get('/user-list', setToken, async (req, res) => {
  const { userData } = req;
  try {
    if (userData.role !== 'admin') {
      throw new Error('you dont have authority');
    }
    const users = await db.findAllUsers();
    res.render('user-list', { userData, users });
  } catch (err) {
    res.render('error', { message: `could not load page: ${err.message}` });
  }
});

export default getRouter;
