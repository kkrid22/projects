import mysql from 'mysql2/promise.js';

export class RequestRepo {
  constructor() {
    this.pool = mysql.createPool({
      connectionLimit: 10,
      database: 'courts',
      host: 'localhost',
      port: 12000,
      user: 'root',
      password: 'almafa',
    });
  }

  // creating the necessary tables
  async createTables() {
    const createPalyakTable = `
        CREATE TABLE IF NOT EXISTS palyak (
          PalyaID INT AUTO_INCREMENT PRIMARY KEY,
          Nev VARCHAR(20),
          Ar INT,
          Cime VARCHAR(100),
          Leiras VARCHAR(255),
          NyitasiIdo TIME,
          ZarasiIdo TIME
        );
      `;
    const createFelhasznalokTable = `
        CREATE TABLE IF NOT EXISTS felhasznalok (
          FelhasznaloID INT AUTO_INCREMENT PRIMARY KEY,
          Nev VARCHAR(50),
          Jelszo VARCHAR(255),
          Szerepkor VARCHAR(20)
        );
      `;

    const createFoglalasokTable = `
        CREATE TABLE IF NOT EXISTS foglalasok (
          FoglalasID INT AUTO_INCREMENT PRIMARY KEY,
          PalyaID INT,
          FelhasznaloID INT,
          Datum DATE,
          Elfogadva BOOLEAN NOT NULL DEFAULT FALSE,
          FOREIGN KEY (PalyaID) REFERENCES palyak(PalyaID),
          FOREIGN KEY (FelhasznaloID) REFERENCES felhasznalok(FelhasznaloID)
        );
      `;

    const createFenykepekTable = `
        CREATE TABLE IF NOT EXISTS fenykepek (
          FenykepID INT AUTO_INCREMENT PRIMARY KEY,
          Path VARCHAR(255),
          PalyaID INT,
          FOREIGN KEY (PalyaID) REFERENCES palyak(PalyaID)
        );
      `;

    try {
      await this.pool.query(createPalyakTable);
      await this.pool.query(createFelhasznalokTable);
      await this.pool.query(createFoglalasokTable);
      await this.pool.query(createFenykepekTable);
      console.log('Tables created successfully');
    } catch (error) {
      console.error('Error creating tables:', error);
      throw error;
    }
  }

  async findAllCourts() {
    const query = 'SELECT palyak.PalyaID, palyak.Nev, palyak.Ar, palyak.Cime FROM palyak';
    const [rows] = await this.pool.query(query);
    return rows;
  }

  async filterCourts(courtType, minPrice, maxPrice) {
    if (courtType !== 'whatever') {
      const query = 'SELECT PalyaID, Nev, Ar, Cime, Leiras FROM palyak WHERE Nev = ? AND Ar >= ? AND Ar <= ?';
      const [rows] = await this.pool.query(query, [courtType, minPrice, maxPrice]);
      return rows;
    }

    const query = 'SELECT PalyaID, Nev, Ar, Cime, Leiras FROM palyak WHERE Ar >= ? AND Ar <= ?';
    const [rows] = await this.pool.query(query, [minPrice, maxPrice]);
    return rows;
  }

  async findCourtDetails(id) {
    const query = `SELECT PalyaID, Nev, Ar, Cime, Leiras 
                   FROM palyak    
                   WHERE PalyaID = ?`;
    const [rows] = await this.pool.query(query, [id]);
    return rows[0];
  }

  async findCourtPhotos(id) {
    const query = `SELECT Path 
                  FROM fenykepek 
                  WHERE PalyaID = ?`;
    const [rows] = await this.pool.query(query, [id]);

    return rows.map((row) => row.Path);
  }

  async findReservations(id, dbStartDate, dbEndDate) {
    const query = `SELECT foglalasok.Datum, felhasznalok.Nev, foglalasok.Elfogadva
                   FROM foglalasok
                   JOIN felhasznalok ON felhasznalok.FelhasznaloID = foglalasok.FelhasznaloID
                   WHERE foglalasok.PalyaID = ? AND DATE(foglalasok.Datum) >= DATE(?) AND DATE(foglalasok.Datum) <= DATE(?)
                   `;
    const [result] = await this.pool.query(query, [id, dbStartDate, dbEndDate]);
    return result;
  }

  async findUser(username) {
    const query = `Select Nev, Jelszo, Szerepkor 
                   From felhasznalok
                   WHERE felhasznalok.Nev = ?
                   `;
    const [users] = await this.pool.query(query, [username]);
    return users[0];
  }

  async findUserID(username) {
    const query = `Select FelhasznaloID
                   From felhasznalok
                   WHERE felhasznalok.Nev = ?
                   `;
    const [users] = await this.pool.query(query, [username]);
    return users;
  }

  async insertCourt(req) {
    const query = 'INSERT INTO palyak VALUES (default, ?, ?, ?, ?, ?, ?)';
    const [response] = await this.pool.query(query, [
      req.Nev,
      req.Ar,
      req.Cim,
      req.Leiras,
      req.NyitasiIdo,
      req.ZarasiIdo,
    ]);
    return response;
  }

  async insertReservation(courtid, userid, rentDate) {
    const query = 'INSERT INTO foglalasok VALUES (default, ?, ?, ?, FALSE)';
    const result = await this.pool.query(query, [courtid, userid, rentDate]);
    return result;
  }

  async insertPicture(id, filename) {
    const query = 'INSERT INTO fenykepek VALUES(default,?,?)';
    const result = await this.pool.query(query, [filename, id]);
    return result;
  }

  async getMissingDetais(id) {
    const query = 'SELECT Leiras, NyitasiIdo, ZarasiIdo FROM palyak WHERE palyak.PalyaID = ?';
    const [result] = await this.pool.query(query, [id]);
    return result;
  }

  async deleteImage(name) {
    const query = 'DELETE FROM fenykepek WHERE Path = ?';
    await this.pool.query(query, [name]);
  }

  async insertUser(username, password) {
    const query = `INSERT INTO felhasznalok
                   VALUES(default, ?, ?, ?)
                   `;
    const role = 'guest';
    await this.pool.query(query, [username, password, role]);
  }

  async findRegisteredUser(username, password) {
    const query = `SELECT Nev, Jelszo, Szerepkor 
                   FROM felhasznalok
                   WHERE felhasznalok.Nev = ? AND felhasznalok.Jelszo = ?
                   `;
    const [row] = await this.pool.query(query, [username, password]);
    return row[0];
  }

  async deleteReservation(userID, date) {
    const query = `DELETE FROM foglalasok
                   WHERE foglalasok.FelhasznaloID = ? AND foglalasok.Datum = ?`;
    await this.pool.query(query, [userID, date]);
  }

  async courtRentedForThatDay(courtID, date) {
    const query = `SELECT Datum, FelhasznaloID
                   FROM foglalasok 
                   WHERE DATE(Datum) = DATE(?) AND PalyaID = ?`;
    const [row] = await this.pool.query(query, [date, courtID]);
    return row[0];
  }

  async changeUserRole(username) {
    const query = `UPDATE felhasznalok
                   SET Szerepkor = 'user'
                   WHERE Nev = ?`;
    const [row] = await this.pool.query(query, [username]);
    return row;
  }

  async disableUser(username) {
    const query = `UPDATE felhasznalok
                   SET Szerepkor = 'deleted'
                   WHERE Nev = ?`;
    const [row] = await this.pool.query(query, [username]);
    return row;
  }

  async blockUser(userID) {
    const query = `UPDATE felhasznalok
                   SET Szerepkor = 'deleted'
                   WHERE FelhasznaloID = ?`;
    const [row] = await this.pool.query(query, [userID]);
    return row;
  }

  async findGuests() {
    const query = `SELECT Nev, Szerepkor 
                   FROM felhasznalok
                   WHERE Szerepkor = 'guest'`;
    const [rows] = await this.pool.query(query);
    return rows;
  }

  async updateUser(username, newUsername, password) {
    const query = `UPDATE felhasznalok
                   SET Nev = ?, Jelszo = ?
                   WHERE Nev = ?`;
    await this.pool.query(query, [newUsername, password, username]);
  }

  async acceptReservation(courtID, date) {
    const query = `UPDATE foglalasok
                   SET Elfogadva = TRUE
                   WHERE PalyaID = ? AND Datum = ?`;
    await this.pool.query(query, [courtID, date]);
  }

  async findAllUsers() {
    const query = `SELECT Nev, FelhasznaloID
                   FROM felhasznalok
                   WHERE Szerepkor != 'deleted'`;
    const [rows] = await this.pool.query(query);
    return rows;
  }
}

const db = new RequestRepo();

try {
  await db.createTables();
} catch (err) {
  console.error(`Create table error: ${err}`);
  process.exit(1);
}

export default db;
