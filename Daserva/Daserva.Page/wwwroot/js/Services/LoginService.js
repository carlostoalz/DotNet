'use strict';

AppDaservaLogin.service("loginService", function ($http) {
    this.login = function (loginDTO) {

    };

    this.GuardarRegistro = function (RegistroDTO) {
        var URL = service + registroUsuarioURL;
        var request = $http({
            method: 'POST',
            url: URL,
            headers: {
                'Content-Type': 'application/json'
            },
            data: RegistroDTO
        });
        return request;
    };
});