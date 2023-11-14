$(document).ready(function () {
    $("#Cover").on("change", function () {
        $(".image_preview").attr("src", window.URL.createObjectURL(this.files[0])).removeClass("d-none")
    });

});