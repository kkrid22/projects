export default function handleNotFound(req, res) {
  res.status(404).render('error', { message: 'requested endpoint is not foind' });
}
