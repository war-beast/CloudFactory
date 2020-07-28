import Vue from "vue";
import MainPageComponent from "Components/currenciesTable/mainPage.vue";

class AppCore {
	private instance: Vue;

	constructor() {
		this.init();
	}

	private init() {
		this.instance = new Vue({
			el: "#vue-app-container",
			render: (h: any) => h(MainPageComponent)
		});
	}
}

new AppCore();