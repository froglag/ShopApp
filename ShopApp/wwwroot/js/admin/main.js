var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        productModel: {
            name: 'product',
            description: 'description',
            price: 0.99
        },
        products: []
    },
    methods: {
        getProducts() {
            this.loading = true;
            axios.get('admin/products')
                .then(res => {
                    console.log(res.data);
                    this.products = res.data;
                })
                .catch(err => { console.log(err); })
                .then(() => { this.loading = false; });
        },
        creatProduct() {
            this.loading = true;
            axios.post('admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                })
                .catch(err => { console.log(err); })
                .then(() => { this.loading = false; });
        },
    }
});
