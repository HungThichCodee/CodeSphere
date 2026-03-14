﻿let lastScrollTop = 0;
const header = document.getElementById("header");

window.addEventListener("scroll", function () {
    let currentScroll = window.pageYOffset || document.documentElement.scrollTop;

    if (currentScroll > lastScrollTop) {
        // Lăn xuống → Ẩn header
        header.classList.add("hidden");
    } else {
        // Lăn lên → Hiện header
        header.classList.remove("hidden");
    }

    lastScrollTop = currentScroll <= 0 ? 0 : currentScroll; // tránh số âm
});