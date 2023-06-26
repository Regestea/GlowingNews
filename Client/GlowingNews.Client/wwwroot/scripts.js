// Set the src attribute of a video element
window.setVideoSrc = (videoElement, url) => {
    videoElement.src = url;
};

// Load a video element
window.loadVideo = (videoElement) => {
    videoElement.load();
};

function scrollToTop() {
    window.scrollTo(5, 0);
}

window.scrollIntercept = {
    nextOrPrevious: function () {
        var scrollPosition = window.pageYOffset;
        var pageHeight = document.documentElement.scrollHeight;
        var windowHeight = window.innerHeight;
        var nextOrPrevious = null;
        if (scrollPosition + windowHeight + 500 >= pageHeight) {
            nextOrPrevious = true;
        }
        if (scrollPosition == 0) {
            nextOrPrevious = false;
        }
        return nextOrPrevious;
    }
}

