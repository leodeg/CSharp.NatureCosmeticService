
$(document).ready(function () {

	$("#button-submit").click(function (e) {
		e.preventDefault();
		var formData = $("#contact-modal-form").serialize();

		$.ajax({
			type: "post",
			url: '/api/ContactUs/',
			data: formData,
		}).done(function () {
			$('#contact-modal-form').each(function () {
				this.reset();
			});
			toastr.success("Ваша заявка принята");
		}).fail(function () {
			toastr.clear();
			if (formData.indexOf('=&') > -1)
				toastr.warning("Пожалуйста, заполните все поля формы.");
		});
		return false;
	})
});