<template>
	<div class="container-fluid">
		<div class="row">
			<button type="button" class="btn btn-primary" v-on:click="getFile">Получить файл</button>

			<div v-if="file !== null" class="text-left">
				{{file}}
			</div>
		</div>
	</div>
</template>

<script lang="ts" src="./mainPage.ts"></script>