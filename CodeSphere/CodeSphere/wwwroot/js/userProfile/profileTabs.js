$(document).ready(function () {
    // L?y username t? URL ho?c t? element
    const currentUsername = $('#currentUsername').text().replace('@', '');
    let currentPage = 1;
    let currentTab = 'Activities';

    // X? l? click trên tab
    $('.nav-pills a').on('click', function (e) {
        e.preventDefault();
        
        // L?y tab name và page t? data attributes
        const tab = $(this).data('tab');
        const page = $(this).data('page') || 1;
        
        console.log('Tab clicked:', tab, 'Page:', page);
        
        // C?p nh?t tr?ng thái active
        $('.nav-pills li').removeClass('active');
        $(this).parent().addClass('active');
        
        // Load n?i dung tab
        loadTabContent(currentUsername, tab, page);
        
        // C?p nh?t URL không reload trang
        const newUrl = `/Profile/${currentUsername}/${tab}/${page}`;
        window.history.pushState({ tab: tab, page: page }, '', newUrl);
        
        currentTab = tab;
        currentPage = page;
    });

    // X? l? nút back/forward c?a tr?nh duy?t
    window.addEventListener('popstate', function (e) {
        if (e.state) {
            loadTabContent(currentUsername, e.state.tab, e.state.page);
            
            // C?p nh?t active tab
            $('.nav-pills li').removeClass('active');
            $(`.nav-pills a[data-tab="${e.state.tab}"]`).parent().addClass('active');
            
            currentTab = e.state.tab;
            currentPage = e.state.page;
        }
    });

    // Hàm load n?i dung tab qua AJAX
    function loadTabContent(username, tab, page) {
        console.log('Loading tab content:', username, tab, page);
        
        // Hi?n th? loading indicator
        $('.tab-content').html('<div class="text-center" style="padding: 50px 0;"><i class="fas fa-spinner fa-spin fa-3x"></i><p style="margin-top: 20px;">Loading...</p></div>');
        
        $.ajax({
            url: `/Profile/LoadTabContent/${username}/${tab}/${page}`,
            type: 'GET',
            success: function (data) {
                console.log('Content loaded successfully');
                $('.tab-content').html(data);
                
                // Re-bind các event handlers sau khi load content m?i
                bindPaginationEvents();
            },
            error: function (xhr, status, error) {
                console.error('Error loading content:', error);
                console.error('Status:', status);
                console.error('Response:', xhr.responseText);
                $('.tab-content').html('<div class="alert alert-danger">Error loading content. Please try again.</div>');
            }
        });
    }

    // Bind events cho pagination links
    function bindPaginationEvents() {
        $('.tab-content .pagination a').on('click', function (e) {
            e.preventDefault();
            
            const href = $(this).attr('href');
            console.log('Pagination clicked:', href);
            
            // Parse URL ð? l?y page number
            // URL format: ?page=2 ho?c /Profile/Index/username?page=2
            let page = 1;
            
            if (href.indexOf('page=') !== -1) {
                const match = href.match(/page=(\d+)/);
                if (match) {
                    page = parseInt(match[1]);
                }
            }
            
            console.log('Going to page:', page);
            
            // Load n?i dung v?i trang m?i
            loadTabContent(currentUsername, currentTab, page);
            
            // C?p nh?t URL
            const newUrl = `/Profile/${currentUsername}/${currentTab}/${page}`;
            window.history.pushState({ tab: currentTab, page: page }, '', newUrl);
            
            currentPage = page;
            
            // Scroll to top c?a tab content
            $('html, body').animate({
                scrollTop: $('.profile-info-right').offset().top - 100
            }, 500);
        });
    }

    // Kh?i t?o state ban ð?u t? URL hi?n t?i
    function initializeState() {
        const pathname = window.location.pathname;
        const pathParts = pathname.split('/').filter(p => p);
        
        console.log('Initial pathname:', pathname);
        console.log('Path parts:', pathParts);
        
        // URL format: /Profile/username/tab/page
        const profileIndex = pathParts.indexOf('Profile');
        if (profileIndex !== -1 && pathParts.length > profileIndex + 2) {
            const tab = pathParts[profileIndex + 2];
            const page = parseInt(pathParts[profileIndex + 3]) || 1;
            
            // Ki?m tra xem tab có h?p l? không
            const validTabs = ['Activities', 'Followers', 'Following', 'Favorites', 'PendingPosts', 'BannedPosts'];
            if (validTabs.includes(tab)) {
                currentTab = tab;
                currentPage = page;
                
                // Set active tab
                $('.nav-pills li').removeClass('active');
                $(`.nav-pills a[data-tab="${tab}"]`).parent().addClass('active');
            }
        } else {
            // L?y tab ðang active
            const activeTab = $('.nav-pills li.active a').first();
            if (activeTab.length > 0) {
                currentTab = activeTab.data('tab') || 'Activities';
                currentPage = 1;
            }
        }
        
        console.log('Initial tab:', currentTab, 'Page:', currentPage);
        
        // Set initial history state
        const currentUrl = `/Profile/${currentUsername}/${currentTab}/${currentPage}`;
        window.history.replaceState({ tab: currentTab, page: currentPage }, '', currentUrl);
    }
    
    initializeState();
    
    // Bind pagination events cho n?i dung ban ð?u
    bindPaginationEvents();
});
