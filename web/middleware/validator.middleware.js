export function validateTime(req, res, next) {
  const { openingTime, closingTime } = req.body;

  if (/[a-zA-Z]/.test(openingTime) || /[a-zA-Z]/.test(closingTime)) {
    return res.status(400).render('error', { message: 'Time cannot be empty and cannot contain letters' });
  }

  if (!openingTime && !closingTime) {
    req.body.openingTime = '00:00:00';
    req.body.closingTime = '24:00:00';
    return next();
  }

  if (!openingTime || !closingTime) {
    return res.status(400).render('error', { message: 'A good time was not given, either fill out both or none' });
  }

  req.body.openingTime += ':00';
  req.body.closingTime += ':00';
  return next();
}

export function validateRent(req, res, next) {
  let { rentingPrice: rent } = req.body;

  if (/[a-zA-Z]/.test(rent) || !rent || Number.isNaN(parseFloat(rent))) {
    return res.status(400).render('error', { message: 'Renting price cannot be empty and cannot contain letters' });
  }

  rent = parseFloat(rent);
  if (rent < 0) {
    return res.status(400).render('error', { message: 'Renting price cannot be negative' });
  }

  req.body.rentingPrice = rent;
  return next();
}

export function validateAddress(req, res, next) {
  const { address } = req.body;

  if (!address) {
    return res.status(400).render('error', { message: 'The address field cannot be empty' });
  }

  return next();
}

export function validateDescription(req, res, next) {
  const { description } = req.body;

  if (!description) {
    return res.status(400).render('error', { message: 'The description field cannot be empty' });
  }

  return next();
}
