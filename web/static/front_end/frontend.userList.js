function blockUser(event) {
  const usertID = event.target.getAttribute('data-userID');

  fetch(`/userBlock?userID=${usertID}`, {
    method: 'DELETE',
  })
    .then((res) => {
      if (!res.ok) {
        throw new Error(`Failed to update reservation: ${res.statusText}`);
      }
      event.target.remove();
    })
    .catch((err) => {
      alert(`Error: ${err}`);
    });
}

window.onload = () => {
  const deleteButtons = document.querySelectorAll('.felhaszBlock');
  deleteButtons.forEach((button) => {
    button.addEventListener('click', blockUser);
  });
};
