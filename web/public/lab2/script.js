function handleInputEvent() {
  const figyeltSzoveg = document.getElementById('figyelt').value;
  const szoveg = document.getElementById('szoveg').value;
  const checkBox = document.getElementById('sensitive');
  const recoloredText = document.getElementById('result');

  if (checkBox.checked) {
    while (recoloredText.firstChild) {
      recoloredText.removeChild(recoloredText.firstChild);
    }

    const figyeltFelbont = figyeltSzoveg.split(' ');
    const szovegFelbont = szoveg.split(' ');
    szovegFelbont.forEach((word) => {
      let found = false;
      figyeltFelbont.forEach((szo) => {
        if (!found) {
          if (word.includes(szo)) {
            const szines = document.createElement('span');
            szines.textContent = word;
            szines.textContent += ' ';
            szines.style.color = 'red';
            recoloredText.appendChild(szines);
            found = true;
          }
        }
      });
      if (!found) {
        const sima = document.createTextNode(word);
        sima.textContent += ' ';
        recoloredText.appendChild(sima);
      }
    });
  } else {
    while (recoloredText.firstChild) {
      recoloredText.removeChild(recoloredText.firstChild);
    }

    const figyeltFelbont = figyeltSzoveg.split(' ');
    const szovegFelbont = szoveg.split(' ');
    szovegFelbont.forEach((word) => {
      let found = false;
      figyeltFelbont.forEach((szo) => {
        if (!found) {
          const wordMasolat = word.toUpperCase();
          const szoMasolat = szo.toUpperCase();

          if (wordMasolat.includes(szoMasolat)) {
            const szines = document.createElement('span');
            szines.textContent = word;
            szines.textContent += ' ';
            szines.style.color = 'red';
            recoloredText.appendChild(szines);
            found = true;
          }
        }
      });
      if (!found) {
        const sima = document.createTextNode(word);
        sima.textContent += ' ';
        recoloredText.appendChild(sima);
      }
    });
  }
}
document.addEventListener('input', handleInputEvent);

window.onload = handleInputEvent;
