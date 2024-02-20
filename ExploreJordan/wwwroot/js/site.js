let slideIndex = 0;
const slides = document.getElementsByClassName("slide");

function showSlides() {
    for (let i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slideIndex++;
    if (slideIndex > slides.length) {
        slideIndex = 1;
    }
    slides[slideIndex - 1].style.display = "block";
    setTimeout(showSlides, 5000);
}

showSlides();

document.addEventListener("DOMContentLoaded", function () {
    const signInIcon = document.getElementById("signin");
    const dropdownContent = document.getElementById("dropdown-content");

    signInIcon.addEventListener("click", function (event) {
        // Toggle the visibility of the dropdown content
        if (dropdownContent.style.display === "block") {
            dropdownContent.style.display = "none";
        } else {
            dropdownContent.style.display = "block";
        }

        // Prevent the click event from closing the dropdown immediately
        event.stopPropagation();
    });

    // Close the dropdown if the user clicks outside of it
    window.addEventListener("click", function () {
        dropdownContent.style.display = "none";
    });
});
