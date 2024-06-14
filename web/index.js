import express from 'express';
import path from 'path';
import morgan from 'morgan';
import auth from './authentication/auth.js';
import errorMidleware from './middleware/error.middleware.js';
import getRouter from './routes/get.requests.rout.js';
import postRouter from './routes/post.request.route.js';
import deleteRouter from './routes/delete.request.route.js';

const app = express();

app.use(express.static(path.join(process.cwd(), 'static')));

app.set('view engine', 'ejs');
app.set('views', path.join(process.cwd(), 'views'));

app.use(morgan('tiny'));

app.use(auth);
app.use(getRouter);
app.use(postRouter);
app.use(deleteRouter);

app.use(errorMidleware);

app.listen(8080, () => {
  console.log('Server listening on http://localhost:8080/ ...');
});
