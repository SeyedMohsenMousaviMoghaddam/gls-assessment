<template>
  <q-layout>
    <q-page-container>
      <q-page class="flex bg-image flex-center">
        <q-card v-bind:style="$q.screen.lt.sm?{'width': '80%'}:{'width':'30%'}">
          <q-card-section>
            <q-avatar size="103px" class="absolute-center shadow-10">
              <img src="profile.svg">
            </q-avatar>
          </q-card-section>
          <q-card-section>
            <div class="text-center q-pt-lg">
              <div class="col text-h6 ellipsis">
                Log in
              </div>
            </div>
          </q-card-section>
          <q-card-section>
            <q-form
              class="q-gutter-md"
            >
              <q-input
                filled
                v-model="username"
                label="Username"
                lazy-rules
              />

              <q-input
                type="password"
                filled
                v-model="password"
                label="Password"
                lazy-rules

              />

              <div>
                <q-btn label="Login"  @click="login" type="button" color="primary"/>
              </div>
            </q-form>
          </q-card-section>
        </q-card>
      </q-page>
    </q-page-container>
  </q-layout>
</template>

<script>
import {defineComponent} from 'vue'
import {ref} from 'vue'
import UserDataService from '../services/UserDataService';
import { useQuasar } from 'quasar'
import { useRoute } from 'vue-router'

export default {
  name: "Login",
  setup() {
    const $q = useQuasar()
    function alarm (message) {
      $q.notify({
                    type: 'negative',
                    message: message
                })
    }
    return {
      alarm
    }
  },  
  data() {
    return {
      username: ref(''),
      password: ref('')
    };
  },  
  methods : {
    login() {
      var data = {
        username: this.username,
        password: this.password
      };
      UserDataService.loginUser(data)
        .then(response => {
          if(response.data.success)
          {
              //localStorage setItem
              if ("localStorage" in window) {
                  localStorage.setItem("access_token",response.data.data);
                  this.$router.push('/Dashboard' );
              } else {
                    console.alarm("no localStorage in window");
              }
          }
          else
          {
            this.alarm(response.data.message);
          }
        })
        .catch(e => {
          console.log(e);
          if(e.response.status == 400)
          {
            this.modelstate = e.response.data.map(function(item){return item.errorMessage;});
            this.modelstate.forEach((element) => {
              this.alarm(element);
             });
          }
          
        });
    }
    /*return {
      username: ref('Pratik'),
      password: ref('12345')
    }*/
  },
}

</script>

<style>

.bg-image {
  background-image: linear-gradient(135deg, #7028e4 0%, #e5b2ca 100%);
}
</style>
