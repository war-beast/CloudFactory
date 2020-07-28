import Vue from "vue";
import MainPageComponent from "Components/main/mainPage.vue";
class AppCore {
    constructor() {
        this.init();
    }
    init() {
        this.instance = new Vue({
            el: "#vue-app-container",
            render: (h) => h(MainPageComponent)
        });
    }
}
new AppCore();
//# sourceMappingURL=init.js.map