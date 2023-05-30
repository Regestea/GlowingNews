// Set the src attribute of a video element
window.setVideoSrc = (videoElement, url) => {
    videoElement.src = url;
};

// Load a video element
window.loadVideo = (videoElement) => {
    videoElement.load();
};