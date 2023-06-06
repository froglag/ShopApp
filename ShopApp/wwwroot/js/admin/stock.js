var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        products: [],
        newStock: {
            productId: 0,
            description: "Size",
            qty: 10
        },
        selectedProduct: null,
    },

    mounted() {
        this.getStock();
    },

    methods: {
        getStock() {
            this.loading = true;
            axios.get('/admin/stocks')
                .then(res => {
                    console.log(res.data);
                    this.products = res.data;
                })
                .catch(err => { console.log(err); })
                .then(() => { this.loading = false; })
        },
        selectProduct(product) {
            this.selectedProduct = product;
            this.newStock.productId = product.id;
        },

        updateStock() {
            this.loading = true;
            axios.put('/admin/stocks', {
                stock: this.selectedProduct.stock.map(x => {
                    return {
                        id: x.id,
                        description: x.description,
                        productId: this.selectedProduct.id,
                        qty: x.qty,
                    };
                })
            })
                .then(res => {
                    console.log(res.data);
                    this.selectedProduct.stock.splice(index, 1);
                })
                .catch(err => { console.log(err); })
                .then(() => { this.loading = false; })
        },

        createStock() {
            this.loading = true;
            axios.post('/admin/stocks', this.newStock)
                .then(res => {
                    console.log(res.data);
                    this.selectedProduct.stock.push(res.data);
                })
                .catch(err => { console.log(err); })
                .then(() => {
                    this.loading = false;
                })
        },
        deleteStock(id, index) {
            this.loading = true;
            axios.delete('/admin/stocks/' + id)
                .then(res => {
                    console.log(res.data);
                    this.selectedProduct.stock.splice(index, 1);
                })
                .catch(err => { console.log(err); })
                .then(() => { this.loading = false; })
        }
    }
});

