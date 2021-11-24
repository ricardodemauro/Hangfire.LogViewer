window.onload = function () {
    const ComponentsApp = {
        data() {
            return {
                logsList: [],
                logCategory: []
            }
        },
        computed: {
            orderedCategories() {
                return this.logCategory
            }
        },
        methods: {
            addLogEvent(logEvent) {
                const l = Object.assign({}, logEvent, { id: this.logsList.length + 1 })
                this.logsList.unshift(l);

                this.addCategory(logEvent);
            },

            addCategory(logEvent) {
                const { HangFireJob: props } = logEvent.properties;

                const hangProps = this.parseProperties(props);

                const c = Object.assign({}, hangProps, { id: vm.logCategory.length + 1 })
                vm.logCategory.unshift(c);
            },

            parseProperties({ Id = '', CreatedAt = '', Type = '', Method = '' }) {
                return {
                    id: Id,
                    createdAt: moment(CreatedAt, 'DD/MM/yyyy HH:mm:ss'),
                    type: Type,
                    method: Method,
                    count: 1,
                    isActive: false
                }
            }
        }
    }

    const app = Vue.createApp(ComponentsApp)

    app.component('vu-log-item', {
        props: ['item'],
        template: `<li>{{ item.timestamp }} {{ item.renderedMessage }}</li>`
    })

    app.component('vu-log-category', {
        props: ['item'],
        template: `<a :class="classObject">#{{ item.id }} {{ item.method }} <smal>{{ timeDifference }}</smal></a>`,
        computed: {
            classObject() {
                return {
                    active: this.isActive,
                    'list-group-item': true
                }
            },
            timeDifference(timeStamp) {
                const created = this.item.createdAt;

                return created.fromNow();
            }
        }
    })

    window.vm = app.mount('#vue-app')

    var connection = new signalR.HubConnectionBuilder().withUrl("/log-sink").build();

    connection.on("PushEventLog", function (logEvent) {
        //console.table(logEvent)
        console.dir(logEvent)

        vm.addLogEvent(logEvent);
    });

    connection.start();


}