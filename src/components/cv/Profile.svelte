<script lang='ts'>
  import type { CurriculumProfileType, CurriculumSocialType } from "./Curriculum.type"

  import SocialList from './SocialList.svelte'

  export let profile: CurriculumProfileType

  const { social } : { social: CurriculumSocialType[] } = profile
</script>

<header class="resume-header pt-4 pt-md-0">
  <div class="media flex-column flex-md-row">
    <img class="mr-3 img-fluid picture mx-auto" src={ profile.img } alt="">
    <div class="media-body p-4 d-flex flex-column flex-md-row mx-auto mx-lg-0">
      <div class="primary-info">
        <h1 class="name mt-0 mb-1 text-white text-uppercase text-uppercase">{ profile.name }</h1>
        <div class="title mb-3">{ profile.title }</div>
        <ul class="list-unstyled">
          <li class="mb-2"><a href="mailto:{ profile.email }"><i class="far fa-envelope fa-fw mr-2" data-fa-transform="grow-3"></i>{ profile.email }</a></li>
          <!-- svelte-ignore a11y-invalid-attribute -->
          <li><a href="#"><i class="fas fa-mobile-alt fa-fw mr-2" data-fa-transform="grow-6"></i>{ profile.cell }</a></li>
        </ul>
      </div><!--//primary-info-->

      <SocialList {social} />

    </div><!--//media-body-->
  </div><!--//media-->
</header>