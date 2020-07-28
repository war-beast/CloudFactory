import Vue from "vue";
import Component from "vue-class-component";

@Component
export default class MainPage extends Vue {
	private msg: string = "Привет!";

	constructor() {
		super();
	}
}