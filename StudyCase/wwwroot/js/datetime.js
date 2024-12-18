window.onload = function () {
    updateDateTime(); // Sayfa yüklendikten sonra ilk kez saat güncellemesi yapılacak

    // Her saniye güncelleme
    setInterval(updateDateTime, 1000);
};

function updateDateTime() {
    var now = new Date();
    var day = now.getDate().toString().padStart(2, '0');
    var month = (now.getMonth() + 1).toString().padStart(2, '0');
    var year = now.getFullYear();
    var hours = now.getHours().toString().padStart(2, '0');
    var minutes = now.getMinutes().toString().padStart(2, '0');
    var seconds = now.getSeconds().toString().padStart(2, '0');

    var formattedDateTime = `${day}.${month}.${year} ${hours}:${minutes}:${seconds}`;
    document.getElementById('date-time').innerText = formattedDateTime;
}
