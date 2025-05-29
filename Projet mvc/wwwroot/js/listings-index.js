document.addEventListener('DOMContentLoaded', function () {
    const sortSelect = document.querySelector('.sort-form select');
    if (sortSelect) {
        sortSelect.addEventListener('change', function () {
            this.form.submit();
        });
    }

});

// Empêche que le clic remonte et déclenche le lien 'stretched-link'
document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.favorite-form button').forEach(button => {
        button.addEventListener('click', function (e) {
            e.stopPropagation();   
        });
    });
});