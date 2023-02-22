export default{
    data() {
        return {
            n: 0,
            active: 0
        }
    },
    props:['items', 'count', 'type'],
    computed: {
        length: function() {
            return Math.ceil(this.items.length / this.count);
        },
        min: function() {
            if(this.active < 4) {
                return 0;
            } else if ( this.active > this.length - 4) {
                return this.length-5;
            } else  {
                return this.active-2;
            }
        },
        max: function() {
            if (this.length < 6) {
                return this.length;
            } else if(this.active < 4) {
                return 6;
            } else if ( this.active > this.length - 5) {
                return this.length;
            } else  {
                return this.active+3;
            }
        },
        numbers: function() {
            var temp = [];
            for(var i = this.min; i < this.max; i++) {
                temp.push(i);
            }
            return temp
        }
    },
    methods: {
        update: function(n){
            this.active = n;
            this.$dispatch('page-' + this.type, this.active);
        }
    }
}