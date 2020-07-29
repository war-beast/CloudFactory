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
const fileLoadingUrl = "/api/files/?filename=second.txt";
let MainPage = class MainPage extends Vue {
    constructor() {
        super();
        this.msg = "Привет!";
        this.file = null;
        this.apiRequest = new ApiRequest();
    }
    getFile() {
        return __awaiter(this, void 0, void 0, function* () {
            yield this.apiRequest.getData(fileLoadingUrl)
                .then((result) => {
                if (result.success) {
                    this.file = result.value;
                }
                else {
                    console.log(`Ошибка загрузки данных по url: ${fileLoadingUrl}`);
                }
            });
        });
    }
};
MainPage = __decorate([
    Component
], MainPage);
export default MainPage;
//# sourceMappingURL=mainPage.js.map