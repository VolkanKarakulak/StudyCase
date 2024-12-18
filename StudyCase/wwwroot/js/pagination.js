<script>
    window.addEventListener("load", () => {
        const currentPage = @currentPage; // Sunucudan gelen currentPage değeri
    const totalPages = @totalPages; // Sunucudan gelen totalPages değeri

    // Sayfa numarasını güncelle
    document.querySelector('.page-number-display').textContent = `Sayfa: ${currentPage} / ${totalPages}`;
    });
</script>