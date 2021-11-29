window.onload = function () {
    const ComponentsApp = {
        data() {
            return {
                logsList: [],
                logCategory: [],
                lastId: 0
            }
        },
        computed: {
            orderedCategories() {
                return this.logCategory.sort((x1, x2) => x2.jobId - x1.jobId);
            },

            orderedLogs() {
                const fnSort = (x1, x2) => {
                    if (x1.properties.Id > x2.properties.Id) return -1;
                    if (x1.properties.Id < x2.properties.Id) return 1;

                    if (x1.id > x2.id) return -1;
                    if (x1.id < x2.id) return 1;

                    return 0;
                };
                return this.logsList.sort(fnSort);
            },

            classObject() {
                return {
                    'new': false
                }
            }
        },
        methods: {
            addLogEvent(logEvent) {

                const l = Object.assign({}, this.parseLogProperties(logEvent), { id: this.logsList.length + 1, isLast: true })
                this.logsList.unshift(l);

                this.addCategory(l);
            },

            addCategory(formatedLog) {
                const hangProps = this.parseCategoryProperties(formatedLog);

                const newCategory = Object.assign({}, hangProps, { id: vm.logCategory.length + 1, isActive: false })
                this.pushCategory(newCategory);
            },

            pushCategory(newCategory) {
                const existingCategory = this.logCategory.find(x => x.jobId == newCategory.jobId);
                if (!existingCategory) {
                    this.logCategory.push(newCategory);
                }
            },

            parseCategoryProperties({ id = '', timeDifferenceFormat = '', properties }) {
                return {
                    id: id,
                    jobId: parseInt(properties.Id),
                    timeDifferenceFormat,
                    name: properties.Method,
                    count: 1,
                    isActive: false
                }
            },

            parseLogProperties({ id = 0,
                level = 0,
                messageTemplate = '',
                renderedMessage = '',
                timestamp = '',

                properties = {
                    HangFireJob: {
                        CreatedAt: '',
                        Id: '',
                        Method: '',
                        Type: ''
                    },
                    time: ''
                } }) {
                return {
                    id,
                    level,
                    levelFormat: this.levelFormat(level),
                    renderedMessage,
                    timestamp: moment(timestamp),
                    createdFormat: this.createdAtFormat(moment(timestamp)),
                    timeDifferenceFormat: this.timeDifferenceFormat(moment(timestamp)),
                    properties: properties.HangFireJob
                }
            },

            createdAtFormat: timestamp => timestamp.format('YYYY-MM-DD HH:mm:ss'),

            timeDifferenceFormat: timestamp => timestamp.fromNow(),

            levelFormat(level) {
                switch (level) {
                    case 1: return 'VER'
                    case 2: return 'INF'
                    case 3: return 'WAR'
                    case 4: return 'ERR'
                    case 5: return 'FAT'
                    case 6: return 'AA'
                }
            }
        },
    }

    const app = Vue.createApp(ComponentsApp)
    app.config.isCustomElement = tag => tag.startsWith('thead')

    app.component('vu-log-category', {
        props: ['item'],
        template: `<a :class="classObject">
                        #{{ item.jobId }} {{ item.name }}
                        <span class="badge badge-primary badge-pill">{{ item.timeDifferenceFormat }}</span>
                    </a>`,
        computed: {
            classObject() {
                return {
                    active: this.item.isActive,
                    'list-group-item': true,
                    'highlight': true
                }
            }
        }
    })

    window.vm = app.mount('#vue-app')

    var connection = new signalR.HubConnectionBuilder().withUrl("/log-sink").build();

    connection.on("PushEventLog", function (logEvent) {
        console.dir(logEvent)

        vm.addLogEvent(logEvent);
    });

    connection.start();


}