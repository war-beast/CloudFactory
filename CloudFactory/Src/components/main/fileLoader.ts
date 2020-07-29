import { Vue, Component, Prop } from "vue-property-decorator";
import { ApiResult } from "Models/apiResult";
import ApiRequest from "Util/request";

const fileLoadingUrlPrefix = "/api/files/?filename=";

@Component
export default class FileRow extends Vue {
	private file: string | null = null;
	private fileName: string = "";
	private errorMessage: string = "";
	private loadingProcess: boolean = false;

	@Prop()
	private availableFiles: string[];

	private readonly apiRequest: ApiRequest;

	constructor() {
		super();

		this.apiRequest = new ApiRequest();
	}

	private async getFile() {
		this.errorMessage = "";
		this.loadingProcess = true;

		if (this.fileName === "") {
			this.errorMessage = "Выберите имя файла в выпадающем списке!";
			return;
		}

		await this.apiRequest.getData(`${fileLoadingUrlPrefix}${this.fileName}`)
			.then((result: ApiResult) => {
				if (result.success) {
					this.file = result.value;
				} else {
					console.log(`Ошибка загрузки данных по url: ${fileLoadingUrlPrefix}${this.fileName}`);
				}

				this.loadingProcess = false;
			});
	}
}