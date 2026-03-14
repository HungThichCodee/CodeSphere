(function () {
    'use strict';

    console.log('headerHideOnScroll: script loaded');

    var header = document.getElementById('header');
    if (!header) {
        console.warn('headerHideOnScroll: #header not found');
        return;
    }

    // ð?m b?o header ? d?ng fixed n?u CSS khác ghi ðè
    var ensureFixed = function () {
        var cs = getComputedStyle(header);
        if (cs.position !== 'fixed' && cs.position !== 'sticky') {
            header.style.position = 'fixed';
            header.style.top = '0';
            header.style.left = '0';
            header.style.right = '0';
            // preserve existing height
        }
    };
    ensureFixed();

    var hiddenClass = 'header-hidden';
    var lastY = window.scrollY || 0;
    var threshold = 10;
    var wheelAccum = 0;
    var wheelThreshold = 30;

    function showHeader() {
        if (header.classList.contains(hiddenClass)) {
            header.classList.remove(hiddenClass);
            console.log('headerHideOnScroll: show');
        }
    }

    function hideHeader() {
        if (!header.classList.contains(hiddenClass)) {
            header.classList.add(hiddenClass);
            console.log('headerHideOnScroll: hide');
        }
    }

    function onWheel(e) {
        // support both wheel and mousewheel
        var delta = e.deltaY || -e.wheelDelta || 0;
        wheelAccum += delta;
        if (Math.abs(wheelAccum) < wheelThreshold) {
            return;
        }
        if (wheelAccum > 0) {
            hideHeader();
        } else {
            showHeader();
        }
        // decay
        wheelAccum = wheelAccum > 0 ? wheelThreshold / 2 : -wheelThreshold / 2;
    }

    var ticking = false;
    function onScroll() {
        if (!ticking) {
            window.requestAnimationFrame(function () {
                var current = window.scrollY || 0;
                if (current <= threshold) {
                    showHeader();
                } else {
                    if (current < lastY) {
                        showHeader();
                    } else if (current > lastY) {
                        hideHeader();
                    }
                }
                lastY = current;
                ticking = false;
            });
            ticking = true;
        }
    }

    // listen nhi?u lo?i s? ki?n ð? týõng thích
    window.addEventListener('wheel', onWheel, { passive: true });
    window.addEventListener('mousewheel', onWheel, { passive: true });
    window.addEventListener('DOMMouseScroll', onWheel, { passive: true }); // old FF
    window.addEventListener('scroll', onScroll, { passive: true });
    window.addEventListener('touchmove', onScroll, { passive: true });

    window.addEventListener('resize', function () { wheelAccum = 0; }, { passive: true });
})();