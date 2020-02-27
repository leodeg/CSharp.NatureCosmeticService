
$(document).ready(function () {

	$("#contact-button-submit").click(function (e) {
		e.preventDefault();
		var formData = $("#create-contact-modal-form").serialize();

		$.ajax({
			type: "post",
			url: '/api/ContactUs/',
			data: formData,
		}).done(function () {
			$('#contact-modal-close').click();
			$('#create-contact-modal-form').each(function () {
				this.reset();
			});
			toastr.success("Ваша заявка принята");
		}).fail(function () {
			toastr.clear();
			if (formData.indexOf('=&') > -1)
				toastr.warning("Пожалуйста заполните форму.");
		});
		return false;
	})
});