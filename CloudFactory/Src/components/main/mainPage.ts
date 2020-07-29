import Vue from "vue";
import Component from "vue-class-component";
import { ApiResult } from "Models/apiResult";
import ApiRequest from "Util/request";
import FileRow from "Components/main/fileLoader.vue"; 

const filesListUrl = "/api/files/list";

@Component({
	components: {
		fileRow: FileRow
	}
})
export default class MainPage extends Vue {
	private availableFiles: string[] = [];
	private fileRowCount: number = 3;

	private readonly apiRequest: ApiRequest;

	constructor() {
		super();

		this.apiRequest = new ApiRequest();
		setTimeout(() => this.getAvailableFiles(), 0);
	}

	private async getAvailableFiles() {
		await this.apiRequest.getData(filesListUrl)
			.then((result: ApiResult) => {
				if (result.success) {
					this.availableFiles = JSON.parse(result.value);
				} else {
					console.log(`Ошибка загрузки данных по url: ${filesListUrl}`);
				}
			});
	}

	private addRow(): void {
		++this.fileRowCount;
	}

	private deleteRow(): void {
		if (this.fileRowCount > 1)
			--this.fileRowCount;
	}
}