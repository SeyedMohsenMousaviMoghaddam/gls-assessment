<template>
  <q-page class="q-pa-sm">
    <div class="row q-col-gutter-sm">

      <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12">
        <q-card class="card-bg text-white">
          <q-card-section class="text-h6 q-pa-sm">
            <div class="text-h6">Change Password</div>
          </q-card-section>         
          <q-card-section class="q-pa-sm row">
            <q-item class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
              <q-item-section>
                Current Password
              </q-item-section>
            </q-item>
            <q-item class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
              <q-item-section>
                <q-input type="password" dark dense outlined color="white" round
                         v-model="password_dict.oldPassword"
                         label="Current Password"/>
              </q-item-section>
            </q-item>
            <q-item class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
              <q-item-section>
                New Password
              </q-item-section>
            </q-item>
            <q-item class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
              <q-item-section>
                <q-input type="password" dark dense outlined color="white" round v-model="password_dict.newPassword"
                         label="New Password"/>
              </q-item-section>
            </q-item>
            <q-item class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
              <q-item-section>
                Confirm New Password
              </q-item-section>
            </q-item>
            <q-item class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
              <q-item-section>
                <q-input type="password" dark dense outlined round color="white"
                         v-model="password_dict.confirmNewPassword"
                         label="Confirm New Password"/>
              </q-item-section>
            </q-item>
          </q-card-section>
          <q-card-actions align="right">
            <q-btn @click="save(password_dict)" class="text-capitalize bg-info text-white">Change Password</q-btn>
          </q-card-actions>

        </q-card>
      </div>
    </div>
  </q-page>
</template>

<script>
import UserDataService from '../services/UserDataService';
import { useQuasar } from 'quasar'
import { useRoute } from 'vue-router'

export default {
  name: "UserProfile",
  setup() {
    const route = useRoute()

    const $q = useQuasar()
    function alert (message) {
      $q.dialog({
        dark: true,
        title: 'Alert',
        message: message
      }).onOk(() => {
        // console.log('OK')
      }).onCancel(() => {
        // console.log('Cancel')
      }).onDismiss(() => {
        // console.log('I am triggered on both OK and Cancel')
      })
    }
    function alarm (message) {
      $q.notify({
                    type: 'negative',
                    message: message
                })
    }
    return {
      alarm,
      alert
    }
  },
  data(){
    return {
      password_dict: {
        oldPassword :'',
        newPassword :'',
        confirmNewPassword:'' ,
        userId : this.$route.params.id??0
      },
      modelstate : ''
    }
  },
  methods:{
    save(input) {
      if(input.userId)
      {
        UserDataService.changePasswordAllUser(input)
        .then(response => {
          this.alert(response.data.message);
        })
        .catch(e => {
          if(e.response.status == 400)
          {
            this.modelstate = e.response.data.map(function(item){return item.errorMessage;});
            this.modelstate.forEach((element) => {
              this.alarm(element);
             });
          }
        });
      }
      else
      {
        input.userId = 0;
        UserDataService.changePassword(input)
        .then(response => {          
          this.alert(response.data.message);
        })
        .catch(e => {
          if(e.response.status == 400)
          {
            this.modelstate = e.response.data.map(function(item){return item.errorMessage;});
            this.modelstate.forEach((element) => {
                    this.alarm(element);
             });
          }
        });
      }
    }
  }
}
</script>

<style scoped>

.card-bg {
  background-color: #353536;
}
</style>
