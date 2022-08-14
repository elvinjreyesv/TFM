//Chosen+Jquery
$("form").each((index, value) => {
    let validatorInfo = $(value).data('validator');
    if (validatorInfo)
        validatorInfo.settings.ignore = ":hidden:not(.chosen-select)";
});

var TourismApp = TourismApp || {
    Resources: {
        Language: document.cookie.indexOf("CurrentLanguage=ES") !== -1 ? "ES" : "EN",
        Get: new function (key) {

            let LangEs = function (key) {
                var obj = {
                    "_Item_AddHighlighted": "Añadir a elementos destacados",
                };

                value = key in obj ? obj[key] : '';

                return value;
            };

            let LangEn = function (key) {
                var obj = {
                    "_Item_AddHighlighted": "Add to highlighted items",
                };

                value = key in obj ? obj[key] : '';

                return value;
            };


            return function (key) {
                var isSpanish = document.cookie.indexOf("CurrentLanguage=ES") !== -1;
                if (isSpanish) {
                    return ResourcesEs(key);
                }
                else {
                    return ResourcesEn(key);
                }
            };

        }
    },
    Constants: {
        BaseUrl: 'https://localhost:44363/'
    },
    Ajax: new function () {
        let isRequesting = false;
        let token = $("#__AjaxForm [name=__RequestVerificationToken]");
        let doneProcess = function (data, successCallback, errorCallback) {
            isRequesting = false;
            if (data) {
                if (data.targetUrl && data.targetUrl.url) {
                    Swal.close();
                    window.open(data.targetUrl.url, data.targetUrl.external ? '_blank' : '_self');
                    return;
                }
                TourismApp.Message.Close();
                if (data.success && successCallback instanceof Function)
                    successCallback(data);
                else if (!data.success && errorCallback instanceof Function)
                    errorCallback(data);
            } else {
                TourismApp.Message.Close();
            }
        };
        let failProcess = function (data, errorCallback) {
            TourismApp.Message.Close();
            isRequesting = false;

            if (data && errorCallback instanceof Function) {
                errorCallback(data);
            }
        };

        this.Get = function (url, data, successCallback, errorCallback) {

            if (isRequesting) return;

            $.ajax({
                type: 'GET',
                url: url,
                beforeSend: function (request) {
                    isRequesting = true;
                    TourismApp.Message.Loading();

                    if (token)
                        request.setRequestHeader("RequestVerificationToken", token.val());
                },
                data: data
            }).done(function (data) {
                doneProcess(data, successCallback, errorCallback);
            }).fail(function (data) {
                failProcess(data, errorCallback);
            });
        };

        this.Post = function (url, data, successCallback, errorCallback) {
            if (isRequesting) return;

            $.ajax({
                type: 'POST',
                url: url,
                beforeSend: function (request) {
                    TourismApp.Message.Loading();
                    isRequesting = true;
                    if (token)
                        request.setRequestHeader("RequestVerificationToken", token.val());

                },
                data: data
            }).done(function (data) {
                doneProcess(data, successCallback, errorCallback);
            }).fail(function (data) {
                failProcess(data, errorCallback);
            });
        };

    },

    Message: new function () {
        let imageUrl = "/images/Logos/logo-hexagon.png";
        let width = '40%';
        var calculateSwalWidth = function () {
            let windowWidth = $(window).width();
            if (windowWidth < 800 && windowWidth > 650) {
                width = '30%';
            }
            if (windowWidth < 650) {
                width = '40%';
            }
        };

        this.Loading = function (data) {
            calculateSwalWidth();
            var lang = document.cookie.indexOf("CurrentLanguage=ES") != -1;

            let modalTitle = data && data.title ? data.title : "";
            let modalMessage = data && data.message
                ? "<h4>" + data.message + "</h4>"
                : lang
                    ? "<h5>procesando solicitud...</h5>"
                    : "<h5>processing request...</h5>";

            let modalTimer = data && data.duration && data.duration > 0 ? data.duration : 0;

            Swal.fire({
                title: modalTitle,
                imageUrl: TourismApp.Constants.BaseUrl + imageUrl,
                html: modalMessage,
                width: width,
                onOpen: () => {
                    Swal.showLoading();
                },
                allowEscapeKey: false,
                allowOutsideClick: false,
                timer: modalTimer
            });
        };

        function typeMessage(type, data) {
            let modalTitle = data && data.title ? data.title : "";
            let modalMessage = data && data.message ? "<h4>" + data.message + "</h4>" : '';
            let modalTimer = data && data.duration && data.duration > 0 ? data.duration : 0;
            let modalClosable = data && data.isClosable && !data.isClosable ? false : true;

            return {
                title: modalTitle,
                html: modalMessage,
                width: width,
                allowEscapeKey: false,
                allowOutsideClick: false,
                timer: modalTimer,
                type: type,
                showConfirmButton: modalClosable
            };
        }

        function typeConfirmationMessage(type, data) {
            let modalTitle = data && data.title ? data.title : '';
            let modalMessage = data && data.message ? data.message : '';
            let modalType = type ? type : '';
            let cancelBtnText = data && data.cancelBtnText ? data.cancelBtnText : '';
            let acceptBtnText = data && data.acceptBtnText ? data.acceptBtnText : '';

            return {
                title: modalTitle,
                text: modalMessage,
                imageUrl: !modalType ? TourismApp.Constants.BaseUrl + imageUrl : '',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonText: cancelBtnText,
                confirmButtonText: acceptBtnText
            };
        }

        this.Info = function (data) {
            calculateSwalWidth();
            return Swal.fire(typeMessage('info', data));
        };

        this.Success = function (data) {
            calculateSwalWidth();
            return Swal.fire(typeMessage('success', data));
        };

        this.Warn = function (data) {
            calculateSwalWidth();
            return Swal.fire(typeMessage('warning', data));
        };

        this.Error = function (data) {
            calculateSwalWidth();
            return Swal.fire(typeMessage('error', data));
        };

        this.Confirm = function (data, type) {
            calculateSwalWidth();
            Swal.fire({
                title: data.title,
                html: data.message,
                type: type,
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonText: data.cancelBtnText,
                confirmButtonText: data.acceptBtnText
            }).then((result) => {
                if (data) {
                    if (result.value) {
                        if (data.onAccept instanceof Function)
                            data.onAccept();
                    } else
                        if (data.onCancel instanceof Function)
                            data.onCancel();
                }
            });
        };

        this.ExternalRedirect = function (data) {
            calculateSwalWidth();
            Swal.fire({
                title: data.title,
                html: data.message,
                imageUrl: TourismApp.Constants.BaseUrl + imageUrl,
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonText: data.cancelBtnText,
                confirmButtonText: data.acceptBtnText
            }).then((result) => {
                if (data) {
                    if (result.value) {
                        if (data.onAccept instanceof Function)
                            data.onAccept();
                    } else
                        if (data.onCancel instanceof Function)
                            data.onCancel();
                }
            });
        };

        this.Close = function () {
            Swal.close();
        };

        this.ViewBagMessage = function (data) {
            if (data.response && data.response !== "") {
                if (data.status && data.status !== "") {
                    if (data.status === "Success") {
                        return TourismApp.Alert.Message({ message: data.response, type: "success", closeable: true });
                    }
                    else {
                        return TourismApp.Alert.Message({ message: data.response, type: "error", closeable: true });
                    }
                }
                return "";
            }
            return "";
        };

    },

    Alert: new function () {

        this.Message = function (data) {
            let alertMessage = data && data.message ? data.message : '';
            let type = data && data.type === 'error' ?
                'alert-danger' :
                data.type === 'warning' ?
                    'alert-warning' :
                    data.type === 'success' ?
                        'alert-success' :
                        '';;
            let closeable = data && data.closeable ? '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' : '';
            let dismisable = data && data.closeable ? 'alert-dismissible' : '';

            var message = '<div class="alert ' + dismisable + ' ' + type + ' show" role="alert"> <strong>' + alertMessage + '</strong>' + closeable + ' </div>';
            return message;
        };

        this.ValidationError = function (data) {
            let type = data && data.type === 'error' ?
                'text-danger' :
                data.type === 'warning' ?
                    'text-warning' :
                    data.type === 'success' ?
                        'text-success' :
                        '';;
            var message = "<span class = '" + type + "'>" + data.message + "</span>";
            return message;
        };
    },
};

$(document).on('click', '.loading', function () {
    TourismApp.Message.Loading();
});

