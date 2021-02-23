var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
import Vue from "vue";
import Component from "vue-class-component";
import ApiRequest from "Util/request";
import FileRow from "Components/main/fileLoader.vue";
const filesListUrl = "/api/files/list";
const clearCaheUrl = "/api/cache/clear";
let MainPage = class MainPage extends Vue {
    constructor() {
        super();
        this.availableFiles = [];
        this.fileRowCount = 3;
        this.apiRequest = new ApiRequest();
        setTimeout(() => this.getAvailableFiles(), 0);
    }
    getAvailableFiles() {
        return __awaiter(this, void 0, void 0, function* () {
            yield this.apiRequest.getData(filesListUrl)
                .then((result) => {
                if (result.success) {
                    this.availableFiles = JSON.parse(result.value);
                }
                else {
                    console.log(`Ошибка загрузки данных по url: ${filesListUrl}`);
                }
            });
        });
    }
    addRow() {
        ++this.fileRowCount;
    }
    deleteRow() {
        if (this.fileRowCount > 1)
            --this.fileRowCount;
    }
    clearCache() {
        return __awaiter(this, void 0, void 0, function* () {
            yield this.apiRequest.getData(clearCaheUrl)
                .then((result) => {
                if (result.success) {
                    this.availableFiles = JSON.parse(result.value);
                }
                else {
                    console.log(`Ошибка очистки кеша: ${filesListUrl}`);
                }
            });
        });
    }
};
MainPage = __decorate([
    Component({
        components: {
            fileRow: FileRow
        }
    })
], MainPage);
export default MainPage;
//# sourceMappingURL=mainPage.js.map