'use strict';

AppDaservaLogin.controller('loginController', function ($scope, $http, loginService) {
    $scope.login = {};
});

AppDaservaLogin.controller('registroController', function ($scope, $http, loginService) {
    $scope.registro = {};
    $scope.comunas = Comunas;

    $scope.GuardarRegistro = function () {
        $scope.registro.Perfil = {
            strCodigo : "CL"
        };
        $scope.registro.Cliente.logActivo = true;
        $scope.registro.Cliente.strUsuarioCreacion = 'pagina';
        var service = loginService.GuardarRegistro($scope.registro);
        service.then(function (response) {
            if (response.data.exitoso) {
                if (response.data.datos) {
                    AlertaSuccess();
                    OcultarModal();
                    LimpiarCampos();|
                }
            } else {
                AlertaErrorGuardar(response.data.error);
            }
        }), function (error) {
            AlertaError(error);
        };      
    };

    function OcultarModal() {
        $('#registroModal').modal("hide");
    }

    function LimpiarCampos() {
        $scope.registro = {};
    }
});
