import jwt from 'jsonwebtoken';

const secret = '1c28d07215544bd1b24faccad6c14a04';

const setToken = (req, res, next) => {
  const { token } = req.cookies;

  if (!token) {
    req.userData = undefined;
    next();
    return;
  }

  jwt.verify(token, secret, (err, decoded) => {
    if (err) {
      req.userData = undefined;
      next();
      return;
    }

    req.userData = decoded;
    next();
  });
};

export default setToken;
