document.addEventListener('DOMContentLoaded', function () {
    const sortSelect = document.querySelector('.sort-form select');
    if (sortSelect) {
        sortSelect.addEventListener('change', function () {
            this.form.submit();
        });
    }
});