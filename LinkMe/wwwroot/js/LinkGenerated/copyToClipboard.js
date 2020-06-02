window.onload = function () {
    var copyButton = document.getElementById("copyButton");
    var shortLinkInput = document.getElementById("shortLinkInput");
    copyButton.addEventListener("click", copyToClipboard, true);
    shortLinkInput.addEventListener("click", copyToClipboard, true);
}

function copyToClipboard() {
    console.log("klik");
    navigator.clipboard.writeText(shortLinkInput.value).then(() => { console.log("udane"); }, (err) => { console.log(err); });
}