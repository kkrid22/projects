function deleteUser(event) {
  const username = event.target.getAttribute('data-username');
  fetch(`/deleteUser?username=${username}`, {
    method: 'DELETE',
  })
    .then((res) => {
      if (!res.ok) {
        throw new Error(`Failed to delete user: ${res.statusText}`);
      }

      const tableContainer = event.target.closest('tr');
      tableContainer.remove();
    })
    .catch((err) => {
      alert(`Error: ${err}`);
    });
}

function acceptUser(event) {
  const username = event.target.getAttribute('data-username');
  fetch(`/acceptUser?username=${username}`, {
    method: 'POST',
  })
    .then((res) => {
      if (!res.ok) {
        throw new Error(`Failed to accept user: ${res.statusText}`);
      }

      const tableContainer = event.target.closest('tr');
      tableContainer.remove();
    })
    .catch((err) => {
      alert(`Error: ${err}`);
    });
}

window.onload = () => {
  const deleteButtons = document.querySelectorAll('.felhasznaloTorles');
  deleteButtons.forEach((button) => {
    button.addEventListener('click', deleteUser);
  });

  const acceptButton = document.querySelectorAll('.felhasznaloElfogad');
  acceptButton.forEach((button) => {
    button.addEventListener('click', acceptUser);
  });
};
