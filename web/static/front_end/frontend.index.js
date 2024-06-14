function addDetails(rowID, rowElement) {
  fetch(`/getMoreDetails?id=${rowID}`)
    .then((response) => {
      if (!response.ok) {
        throw new Error('Failed to fetch data');
      }
      return response.json();
    })
    .then((data) => {
      if (!data || data.length === 0) {
        throw new Error('No data received');
      }

      const details = data[0];

      if (!details.Leiras || !details.NyitasiIdo || !details.ZarasiIdo) {
        throw new Error('Invalid data structure');
      }

      const existingRow = document.getElementById(`details-${rowID}`);
      if (existingRow) {
        existingRow.remove();
      }

      const newRow = document.createElement('tr');
      newRow.id = `details-${rowID}`;

      const newCell = document.createElement('td');
      newCell.colSpan = 4;

      const detailsContent = document.createElement('div');
      const descriptionText = document.createTextNode(`Description: ${details.Leiras}`);
      const openingHoursText = document.createTextNode(`Opening Hours: ${details.NyitasiIdo}`);
      const closingHoursText = document.createTextNode(`Closing Hours: ${details.ZarasiIdo}`);

      // Append text nodes to the detailsContent div
      const strongElement = document.createElement('strong');
      strongElement.appendChild(document.createTextNode('Additional Details:'));
      detailsContent.appendChild(strongElement);
      detailsContent.appendChild(document.createElement('br'));
      detailsContent.appendChild(descriptionText);
      detailsContent.appendChild(document.createElement('br'));
      detailsContent.appendChild(openingHoursText);
      detailsContent.appendChild(document.createElement('br'));
      detailsContent.appendChild(closingHoursText);

      newCell.appendChild(detailsContent);
      newRow.appendChild(newCell);

      rowElement.parentNode.insertBefore(newRow, rowElement.nextSibling);
    })
    .catch((err) => {
      console.error('Error fetching data:', err);
    });
}

window.onload = () => {
  const detailElements = document.querySelectorAll('.details');
  detailElements.forEach((details) => {
    details.addEventListener('click', (event) => {
      const rowID = event.target.dataset.rowid;
      if (rowID) {
        const rowElement = event.target.closest('tr');
        addDetails(rowID, rowElement);
      }
    });
  });
};
