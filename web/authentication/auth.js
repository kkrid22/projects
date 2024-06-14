import express from 'express';
import bodyParser from 'body-parser';
import cookieParser from 'cookie-parser';
import bcrypt from 'bcrypt';
import jwt from 'jsonwebtoken';
import db from '../db/requests.db.js';

const auth = express.Router();
const secret = '1c28d07215544bd1b24faccad6c14a04';

auth.use(bodyParser.json());
auth.use(cookieParser());
auth.use(express.urlencoded({ extended: true }));

auth.get('/logout', (req, res) => {
  res.clearCookie('token');
  res.redirect('/login.html');
});

auth.post('/login', async (req, res) => {
  const { username, password } = req.body;
  try {
    if (!username) {
      throw new Error('username was not given');
    }

    if (!password) {
      throw new Error('password was not given');
    }

    const user = await db.findUser(username);

    if (user === undefined) {
      throw new Error('wrong username or password');
    }

    const match = await bcrypt.compare(password, user.Jelszo);
    if (!match) {
      throw new Error('wrong username or password');
    }

    const isDisabled = await db.findRegisteredUser(username, user.Jelszo);
    if (isDisabled.Szerepkor === 'deleted') {
      throw new Error('sorry this user has been deleted');
    }

    const token = jwt.sign({ username, role: isDisabled.Szerepkor }, secret, {
      expiresIn: '1h',
    });
    res.cookie('token', token, { httpOnly: true, maxAge: 3600000 });
    res.redirect('/');
  } catch (err) {
    res.status(400).send(`something is wrong: ${err}`);
  }
});

auth.post('/register', async (req, res) => {
  const { username, password, passwordAgain } = req.body;
  try {
    if (!username) {
      throw new Error('username was not given');
    }

    if (!password) {
      throw new Error('password was not given');
    }

    if (password !== passwordAgain) {
      throw new Error('the two passwords are different');
    }

    const user = await db.findUser(username);
    if (user !== undefined) {
      throw new Error('username already in use');
    }

    const saltedPassword = await bcrypt.hash(password, 10);
    await db.insertUser(username, saltedPassword);

    const token = jwt.sign({ username, role: 'guest' }, secret, {
      expiresIn: '1h',
    });
    res.cookie('token', token, { httpOnly: true, maxAge: 3600000 });
    res.redirect('/');
  } catch (err) {
    res.status(400).send(`something is wrong: ${err}`);
  }
});

export default auth;
