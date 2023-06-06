var app = new Vue({
    el: '#app',
    data: {
        editing: false,
        loading: false,
        indexModel:0,
        productModel: {
            id: 0,
            name: 'product',
            description: 'description',
            price: 0.99
        },
        products: []
    },

    mounted() {
        this.getProducts();
    },

    methods: {
        getProduct(id) {
            this.loading = true;
            axios.get('/admin/products/' + id)
                .then(res => {
                    console.log(res.data);
                    this.products = res.data;
                })
                .catch(err => { console.log(err); })
                .then(() => { this.loading = false; })
        },
        getProducts() {
            this.loading = true;
            axios.get('/admin/products')
                .then(res => {
                    console.log(res.data);
                    this.products = res.data;
                })
                .catch(err => { console.log(err); })
                .then(() => { this.loading = false; })
        },
        creatProduct() {
            this.loading = true;
            axios.post('/admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                })
                .catch(err => { console.log(err); })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                })
        },
        updateProduct() {
            this.loading = true;
            axios.put('/admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.splice(this.indexModel, 1, res.data);
                })
                .catch(err => { console.log(err); })
                .then(() => {
                    this.editing = false;
                    this.loading = false;
                })
        },
        editProduct(product, index) {
            this.indexModel = index;
            this.productModel = {
                id: product.id,
                name: product.name,
                description: product.description,
                price: product.price,
            };
            this.editing = true;
        },
        deleteProduct(id, index) {
            this.loading = true;
            axios.delete('/admin/products/'+id)
                .then(res => {
                    console.log(res.data);
                    this.products.splice(index, 1);
                })
                .catch(err => { console.log(err); })
                .then(() => { this.loading = false; })
        },
        newProduct() {
            this.editing = true;
            this.productModel.id = 0;
        },
        cansel() {
            this.editing = false;
        },
    }
});
