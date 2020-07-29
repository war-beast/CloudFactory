<template>
	<div class="row my-4 text-left">
		<div class="d-flex align-items-center col-6">
			<label class="mr-4 flex-nowrap">Выберите файл:</label>
			<select v-model="fileName" class="form-control mr-5">
				<option v-for="(name, index) in availableFiles">{{name}}</option>
			</select>
			<button type="button" class="btn btn-primary" v-on:click="getFile" v-bind:class="{ disabled: loadingProcess }">
				<img src="/img/loader.gif" v-if="loadingProcess" />
				<span v-else>Загрузить</span>
			</button>
		</div>
		<div class="w-100 text-danger" v-if="errorMessage !== ''">{{errorMessage}}</div>

		<div v-if="file !== null" class="mt-4 pb-4 border-bottom">
			{{file}}
		</div>
	</div>
</template>

<script lang="ts" src="./fileLoader.ts"></script>