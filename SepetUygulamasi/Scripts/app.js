/// <reference path="angular.js" />

var app = angular.module("northApp", []);

app.controller("SiparisCtrl",
    function ($scope, $http, $document) {
        $scope.urunler = null;
        $scope.sepet = [];
        $scope.toplam = 0;
        init();

        function init() {
            $http({
                url: '../Siparis/Urunler',
                method: 'GET'
            }).then(function (response) {
                console.log(response);
                if (response.data.success)
                    $scope.urunler = response.data.data;
                else {
                    alert(response.data.message);
                }
            });
        }
        $scope.git = function (sayfa, event) {

            console.log(event);
            $http({
                url: '../Siparis/Urunler?sayfa=' + sayfa,
                method: 'GET'
            }).then(function (response) {
                console.log(response);
                if (response.data.success)
                    $scope.urunler = response.data.data;
                else {
                    alert(response.data.message);
                }
            });
        }
        $scope.function = function (urun) {
            console.log(urun);
            if ($scope.sepet.length === 0) {
                urun.Quantity = 1;
                $scope.sepet.push(urun);
            } else {
                var varmi = false;
                for (var i = 0; i < $scope.sepet.length; i++) {
                    if ($scope.sepet[i].ProductID === urun.ProductID) {
                        varmi = true;
                        $scope.sepet[i].Quantity++;
                        break;
                    }
                }
                if (!varmi) {
                    urun.Quantity = 1;
                    $scope.sepet.push(urun);
                }
            }
            sepethesapla();
        }

        $scope.sepetionayla = function () {
            $http({
                url: '../Siparis/Urunler',
                method: 'POST',
                data: $scope.sepet
            }).then(function (response) {
                console.log(response);
                alert(response.data.message);
                if (response.data.success) {
                    $scope.toplam = 0;
                    $scope.sepet = [];
                }
            });
        }
        function sepethesapla() {
            $scope.toplam = 0;
            for (var i = 0; i < $scope.sepet.length; i++) {
                $scope.toplam += $scope.sepet[i].Quantity * $scope.sepet[i].UnitPrice;
            }
        }
    });