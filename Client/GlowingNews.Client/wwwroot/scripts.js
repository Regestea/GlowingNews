// Set the src attribute of a video element
window.setVideoSrc = (videoElement, url) => {
    videoElement.src = url;
};

// Load a video element
window.loadVideo = (videoElement) => {
    videoElement.load();
};


window.scrollIntercept = {
    isEndOfPageReached: function () {
        var scrollPosition = window.pageYOffset;
        var pageHeight = document.documentElement.scrollHeight;
        var windowHeight = window.innerHeight;
        var isEndOfPageReached = false;
        if (scrollPosition + windowHeight +400 >= pageHeight) {
            isEndOfPageReached = true;
        }
        return isEndOfPageReached;
    }
}