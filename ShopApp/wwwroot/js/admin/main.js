var app = new Vue({
    el: '#app',
    data: {
        loading: false,
    },
    methods: {
        getProducts() {
            this.loading = true;
            axios.get('admin/products')
                .then(res => { console.log(res.data); })
                .catch(err => { console.log(err); })
                .then(() => { this.loading = false; });
        },
    }
});
