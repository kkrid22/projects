function formatDateForDatabase(dateString) {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0'); // kitomi 0 val ameddig a hossza 2 nem lesz
  const day = String(date.getDate()).padStart(2, '0');
  return `${year}-${month}-${day}`;
}

function deleteImage(event) {
  const filename = event.target.id;
  fetch(`/deleteImage?filename=${filename}`, {
    method: 'DELETE',
  })
    .then((res) => {
      if (!res.ok) {
        throw new Error(`Failed to delete image: ${res.statusText}`);
      }

      const imageContainer = event.target.closest('.image-container');
      imageContainer.remove();

      const remainingImages = document.querySelectorAll('.image-container');
      if (remainingImages.length === 0) {
        const message = document.createElement('div');
        message.className = 'message';
        message.textContent = 'No images available';

        const details = document.querySelector('.details');
        details.appendChild(message);
      }
    })
    .catch((err) => {
      alert(`Error: ${err}`);
    });
}

function deleteReserv(event) {
  const courtID = event.target.getAttribute('data-courtID');
  const rawDate = event.target.getAttribute('data-date');
  const date = formatDateForDatabase(rawDate);

  fetch(`/deleteReserv?date=${date}&courtID=${courtID}`, {
    method: 'DELETE',
  })
    .then((res) => {
      if (!res.ok) {
        throw new Error(`Failed to delete reservation: ${res.statusText}`);
      }

      const row = event.target.closest('tr');
      row.remove();

      const remainingRows = document.querySelectorAll('.rented tbody tr');
      if (remainingRows.length === 0) {
        const message = document.createElement('div');
        message.className = 'message';
        message.textContent = 'This court has not been rented';
        const rentedDiv = document.querySelector('.rented');
        rentedDiv.replaceChildren(message);
      }
    })
    .catch((err) => {
      alert(`Error: ${err}`);
    });
}

function acceptReserv(event) {
  const courtID = event.target.getAttribute('data-courtID');
  const rawDate = event.target.getAttribute('data-date');
  const date = formatDateForDatabase(rawDate);

  fetch(`/acceptReserv?date=${date}&courtID=${courtID}`, {
    method: 'POST',
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
  const deleteButtons = document.querySelectorAll('.torles');
  deleteButtons.forEach((button) => {
    button.addEventListener('click', deleteImage);
  });

  const deleteReservation = document.querySelectorAll('.foglalasTorles');
  deleteReservation.forEach((button) => {
    button.addEventListener('click', deleteReserv);
  });

  const acceptReservation = document.querySelectorAll('.foglalasElfogad');
  acceptReservation.forEach((button) => {
    button.addEventListener('click', acceptReserv);
  });
};
