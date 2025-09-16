let isMuted = false;
function playSfx(sfx) {
    if (isMuted) {
        return;
    }
    try {
        let audio = document.getElementById(sfx);
        audio.currentTime = 0;
        audio.play();
    } catch (e) {
        //nom!
    }
}
function toggleMute() {
    isMuted = !isMuted;
}