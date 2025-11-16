document.addEventListener("DOMContentLoaded", function () {
    var autoCloseAlert = document.getElementById("autoCloseAlert");

    if (autoCloseAlert) {
        var alertTime = 3000; 
        var timeLeft = alertTime;
        var intervalTime = 100; 

        var interval = setInterval(function () {
            if (!autoCloseAlert.matches(':hover')) {
                timeLeft -= intervalTime;
            }

            if (timeLeft <= 0) {
                clearInterval(interval);
                var bsAlert = new bootstrap.Alert(autoCloseAlert);
                bsAlert.close();
            }
        }, intervalTime);
    }
});