document.addEventListener('DOMContentLoaded', function () {
    const sortSelect = document.querySelector('.sort-form select');
    if (sortSelect) {
        sortSelect.addEventListener('change', function () {
            this.form.submit();
        });
    }
<<<<<<< HEAD
});

// Empêche que le clic remonte et déclenche le lien 'stretched-link'
document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.favorite-form button').forEach(button => {
        button.addEventListener('click', function (e) {
            e.stopPropagation();   
        });
    });
=======
>>>>>>> 6ee7f94ca1c0bda8cd9255815e500fa4d2f2cba0
});