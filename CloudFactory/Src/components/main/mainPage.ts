import Vue from "vue";
import Component from "vue-class-component";
import { ApiResult } from "Models/apiResult";
import ApiRequest from "Util/request";

const fileLoadingUrl = "/api/files/?filename=second.txt";

@Component
export default class MainPage extends Vue {
	private msg: string = "Привет!";
	private file: string | null = null;

	private readonly apiRequest: ApiRequest;

	constructor() {
		super();

		this.apiRequest = new ApiRequest();
	}

	private async getFile() {
		await this.apiRequest.getData(fileLoadingUrl)
			.then((result: ApiResult) => {
				if (result.success) {
					this.file = result.value;
				} else {
					console.log(`Ошибка загрузки данных по url: ${fileLoadingUrl}`);
				}
			});
	}
}