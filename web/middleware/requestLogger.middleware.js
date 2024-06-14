import db from '../db/requests.db.js';

export async function insertCourtLogger(req, res, next) {
  try {
    const [header] = await db.insertCourt(req);
    console.log(`Inserted request. Affected rows: ${header.affectedRows}`);
    next();
  } catch (err) {
    res.status(500).render('error', { message: `Insertion unsuccsessful: ${err.message}` });
  }
}

export async function insertPictureLogger(req, res, next) {
  try {
    const [header] = await db.insertPicture(req);
    console.log(`Inserted request. Affected rows: ${header.affectedRows}`);
    next();
  } catch (err) {
    res.status(500).render('error', { message: `Insertion unsuccsessful: ${err.message}` });
  }
}

export async function insertReservationLogger(req, res, next) {
  try {
    const [header] = await db.insertReservation(req);
    console.log(`Inserted request. Affected rows: ${header.affectedRows}`);
    next();
  } catch (err) {
    res.status(500).render('error', { message: `Insertion unsuccsessful: ${err.message}` });
  }
}

export async function createTablesLogger(req, res, next) {
  try {
    await db.createTables();
    console.log('creation requested, tables created succesfully');
    next();
  } catch (err) {
    res.status(500).render('error', { message: `Insertion unsuccsessful: ${err.message}` });
  }
}
