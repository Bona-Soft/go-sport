angular.module('HayEquipoApp').service('AlertService', ['$uibModal', '$state', 'toaster', 'SweetAlert', function ($uibModal, $state, toaster, SweetAlert) {

	//Toaster Alert
	this.Alert = function (HttpResponse, msgCustom) {
		if (HttpResponse) {

			toaster.pop({
				'type': getTypeAlert(HttpResponse),
				'title': getTitleAlert(HttpResponse),
				'body': getBodyAlert(HttpResponse, msgCustom),
				'timeout': getTimeoutAlert(HttpResponse),
				'bodyOutputType': 'trustedHtml'
			});
		}
	}

	//SweetAlert Alert
	this.AlertEx = function (type, title, body, timeout) {
		this.ShowMessage(type, title, body, timeout);
	}

	//SweetAlert Alert
	this.ShowMessage = function (type, titulo, texto, timeout) {
		if (timeout == 0)
			timeout = null;

		SweetAlert.swal({
			title: titulo,
			text: texto,
			type: type,
			timer: timeout,  // in ms 0 = infinito
			confirmButtonColor: getButtonColor(type),
			allowOutsideClick: true,
			showConfirmButton: true
		});
	}

	//SweetAlert Confirm
	this.Confirm = function (titulo, texto, type, success, dismiss) {
		//Valido que sea un type valido
		switch (type) {
			case 'primary':
				break;
			case 'info':
				break;
			case 'success':
				break;
			case 'warning':
				break;
			case 'error':
				break;
			default:
				type = 'primary';
		}

		SweetAlert.swal({
			title: titulo,
			text: texto,
			type: type,
			showConfirmButton: true,
			confirmButtonText: 'Aceptar',
			confirmButtonColor: getButtonColor(type),
			showCancelButton: true,
			cancelButtonText: 'Cancelar',
		},
      function (isConfirm) {
      	if (isConfirm) {
      		if (success) {
      			success();
      		}
      	} else {
      		if (dismiss) {
      			dismiss();
      		}
      	}
      });
	}

	function getTypeAlert(HttpResponse) {
		if (HttpResponse) {
			if (HttpResponse.status) {
				switch (HttpResponse.status.toString().substring(0, 1)) {
					case '1': //Informational
						return "info";
					case '2': //Success
						return "success";
					case '3': //Redirection
						return "info";
					case '4': //Client Error
						return "warning";
					case '5': //Server Error
						return "error";
				}
			}
		}
	}

	function getTitleAlert(HttpResponse) {
		if (HttpResponse) {
			if (HttpResponse.status) {
				switch (HttpResponse.status.toString().substring(0, 1)) {
					case '1': //Informational
						return "Informacion";
					case '2': //Success
						return "Operacion exitosa";
					case '3': //Redirection
						return "Atencion";
					case '4': //Client Error
						return "Operacion rechazada";
					case '5': //Server Error
						return "Error";
				}
			}
		}
	}

	function getTimeoutAlert(HttpResponse) {
		if (HttpResponse) {
			if (HttpResponse.status) {
				switch (HttpResponse.status.toString().substring(0, 1)) {
					case '1': //Informational
						return 5000;
					case '2': //Success
						return 2500;
					case '3': //Redirection
						return 5000;
					case '4': //Client Error
						return 0;
					case '5': //Server Error
						return 0;
				}
			}
		}
	}

	function getBodyAlert(HttpResponse, msgCustom) {
		var error = '';

		if (msgCustom != undefined) {
			return msgCustom;
		}

		if (HttpResponse) {
			if (HttpResponse.data) {
				if (HttpResponse.data["mensaje"]) {
					return HttpResponse.data["mensaje"];
				}

				var el = document.createElement('html');
				el.innerHTML = HttpResponse.data;
				if (el.getElementsByTagName('title')[0]) {
					return el.getElementsByTagName('title')[0].text;
				}
			}

			return HttpResponse.statusText;
		}
	}

	function getButtonColor(type) {
		switch (type) {
			case 'primary':
				return '#428bca';
			case 'info':
				return '#31b0d5';
			case 'success':
				return '#5cb85c';
			case 'warning':
				return '#f0ad4e';
			case 'error':
				return '#d9534f';
			default:
				return '#428bca';
		}
	}

}]);

