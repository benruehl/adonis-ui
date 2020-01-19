const debounce = (fn) => {
    let frame;
  
    return (...params) => {
      if (frame) { 
        cancelAnimationFrame(frame);
      }
  
      frame = requestAnimationFrame(() => {
        fn(...params);
      });
    } 
};


// Reads out the scroll position and stores it in the data attribute
// so we can use it in our stylesheets
const storeScroll = () => {
    document.documentElement.dataset.scroll = window.scrollY;
}

// Listen for new scroll events, here we debounce our `storeScroll` function
document.addEventListener('scroll', debounce(storeScroll), { passive: true });

// Update scroll position for first time
storeScroll();

// Toggles the nav-open class on additional elements
const initNavSearch = () => {
    const navTrigger = document.querySelector('.js-main-nav-trigger');
    const headerNav = document.querySelector('.js-header-nav');
    const search = document.querySelector('.js-search');
    
    window.jtd.addEvent(navTrigger, 'click', function(e){
        e.preventDefault();
        headerNav.classList.toggle('nav-open');
        search.classList.toggle('nav-open');
    });
};

jtd.onReady(function(){
    initNavSearch();
});
